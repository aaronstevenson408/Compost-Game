import csv
import json
import queue
from openai import OpenAI

# Replace with your CSV file path
csv_file = "compostables.csv"

# Replace with your desired JSON file path
json_output_file = "compostable_items_results.json"

# Data to hold results
results = []

# OpenAI setup (replace with your API key)
openai = OpenAI(base_url="http://localhost:1234/v1", api_key="YOUR_OPENAI_API_KEY")

schema = """
{
  "type": "object",
  "properties": {
    "itemName": {
      "type": "string",
      "description": "The name of the compostable item"
    },
    "nitrogenContent": {
      "type": "number",
      "description": "Nitrogen content of the item (for green materials), percentage written as decimal",
      "minimum": 0.0,
      "maximum": 1.0
    },
    "carbonContent": {
      "type": "number",
      "description": "Carbon content of the item (for brown materials), percentage written as decimal",
      "minimum": 0.0,
      "maximum": 1.0
    },
    "itemWeight": {
      "type": "number",
      "description": "Average Weight of the item in pounds, be as accurate as possible",
      "minimum": 0.0
    }
  },
  "required": ["itemName", "itemWeight", "nitrogenContent", "carbonContent"]
}
"""

# Queue to hold items that fail the sanity check
retry_queue = queue.Queue()

# Open CSV file
with open(csv_file, 'r') as csvfile:
    reader = csv.reader(csvfile)
    next(reader)  # Skip header row (assuming you have a header)
    for row in reader:
        compostable_item = row[0]  # Assuming the item is in the first column
        retry_queue.put(compostable_item)

# Process items in the queue
while not retry_queue.empty():
    compostable_item = retry_queue.get()
    # System prompt with schema description
    system_prompt = f"""<|im_start|>system
You are a helpful assistant that answers in JSON. Here's the json schema you must adhere to:\n<schema>\n{schema}\n</schema>
<|im_end|>"""

    # Create the message structure
    messages = [
        {"role": "system", "content": system_prompt},
        {"role": "user", "content": compostable_item}
    ]

    try:
        # Send the request using OpenAI library
        completion = openai.chat.completions.create(
            model="NousResearch/Hermes-2-Pro-Mistral-7B-GGUF/Hermes-2-Pro-Mistral-7B.Q8_0.gguf",
            messages=messages,
            temperature=0.7
        )

        # Extract the response and parse it as JSON
        response_json = json.loads(completion.choices[0].message.content)

        # Validate the sum of nitrogenContent and carbonContent
        total_content = response_json["nitrogenContent"] + response_json["carbonContent"]
        print(f"Total Content: {total_content}")
        if total_content > 1 or total_content < 0:
            print(f"Error for item '{compostable_item}': Sum of nitrogenContent and carbonContent should be between 0 and 1.")
            retry_queue.put(compostable_item)  # Re-queue the item for another try
        else:
            results.append(response_json)
            print(f"response:{response_json}")
    except Exception as e:
        print(f"Error for item '{compostable_item}': {e}")
        retry_queue.put(compostable_item)  # Re-queue the item for another try

# Write results to JSON file
with open(json_output_file, 'w') as outfile:
    json.dump(results, outfile, indent=4)

print(f"Results written to {json_output_file}")
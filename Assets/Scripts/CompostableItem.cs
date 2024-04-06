public class CompostableItem
{
    public string itemName;
    public float nitrogenContent; // For "green" materials
    public float carbonContent; // For "brown" materials

    public CompostableItem(string name, float nitrogen, float carbon)
    {
        itemName = name;
        nitrogenContent = nitrogen;
        carbonContent = carbon;
    }
}
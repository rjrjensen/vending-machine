using System;
using System.Linq;

public class VendingMachine(Item[] Items)
{
    public const int Capacity = 10;

    public (Item?, decimal) Vend(int coordinate, decimal cash)
    {
        if (Items[coordinate] is null || Items[coordinate].Value > cash) return (null, cash);

        var vendedItem = Items[coordinate];
        Items[coordinate] = null;

        var change = cash - vendedItem.Value;

        return (vendedItem, change);
    }

    public int AddItems(params Item[] newItems) 
    {
        var itemsList = Items.ToList();
        var initialCount = Items.Length;

        var availableCapacity = Capacity - initialCount;

        var countToAdd = Math.Min(availableCapacity, newItems.Length);
        itemsList.AddRange(newItems[..countToAdd]);
        
        Items = itemsList.ToArray();

        return Items.Length;
    }
}

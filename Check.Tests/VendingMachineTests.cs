using System;
using Xunit;

namespace Check.Tests;

public class VendingMachineTests
{
    [Fact]
    public void GivenTheCorrectAmountOfMoney_WhenAnOrderIsPlaced_ProductIsDispensedWithChange()
    {
        var item = new Item("candy", 1m);
        var items = new Item[] { item };
        var sut = new VendingMachine(items);
        var availableCash = 10m;

        var expectedChange = 9m;

        var (product, change) = sut.Vend(0, availableCash);

        Assert.Equal(change, expectedChange);
        Assert.Equivalent(product, item);
    }

    [Fact]
    public void GivenTheIncorrectAmountOfMoney_WhenAnOrderIsPlaced_ProductIsNotDispensedAndChangeMatchesCash()
    {
        var items = new Item[] { new("candy", 10m) };
        var sut = new VendingMachine(items);
        var availableCash = 1m;

        var expectedChange = 1m;

        var (product, change) = sut.Vend(0, availableCash);

        Assert.Equal(change, expectedChange);
        Assert.Equivalent(product, null);
    }

    [Fact]
    public void GivenOnlyOneItemInMachine_WhenTwoOrdersPlaced_OnlyOneItemVended()
    {
        var item = new Item("candy", 1m);
        var items = new Item[] { item };
        var sut = new VendingMachine(items);
        var availableCash = 10m;

        var expectedChange = 9m;

        var (product, change) = sut.Vend(0, availableCash);
        var (product2, change2) = sut.Vend(0, change);

        Assert.Equal(change, expectedChange);
        Assert.Equivalent(product, item);

        Assert.Equal(change, change2);
        Assert.Equivalent(product2, null);
    }

    [Fact]
    public void GivenInitialInventory_WhenNewItemsAdded_InventoryCountUpdates() 
    {
        var item = new Item("candy", 1m);
        var items = new Item[] { item };
        var sut = new VendingMachine(items);
        var newItem = new Item("candy", 1m);

        var newInventoryCount = sut.AddItems(newItem);

        Assert.Equal(2, newInventoryCount);
    }

    [Fact]
    public void GivenInitialInventory_When15NewItemsAdded_CapacityMaxedOut()
    {
        var item = new Item("candy", 1m);
        var items = new Item[] { item };
        var sut = new VendingMachine(items);

        var newItems = new Item[15];

        for (var i = 0; i < 15; i++)
        {
            newItems[i] = new Item("candy", 1m);
        }

        var newInventoryCount = sut.AddItems(newItems);

        Assert.Equal(VendingMachine.Capacity, newInventoryCount);
    }
}

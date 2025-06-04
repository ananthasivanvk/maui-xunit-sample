using MauiBmiCalculator.ViewModels;
using Xunit;

namespace MauiBmiCalculator.Tests;

public class BmiViewModelTests
{
    [Fact]
    public void CalculateBmi_SetsCorrectCategory()
    {
        var vm = new BmiViewModel();
        vm.Weight = 70;
        vm.Height = 1.75;
        vm.CalculateBmi();
        Assert.Equal("Normal weight", vm.BmiCategory);
    }
}

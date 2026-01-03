namespace GymApp.Shared.Events;

public interface IFoodAddedEvent
{
    public string FoodName { get; set; }
    public DateTime DateAdded { get; set; }
    public double Calories { get; set; }
}
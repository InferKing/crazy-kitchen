public class Pan : Dishes
{
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Cookable cookable)
        {
            AddIngredient(cookable);
            PlaceIngredient(cookable);
            stayInHand = true;
            return true;
        }
        return false;
    }
}

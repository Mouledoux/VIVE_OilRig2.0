public interface IInteractableObject
{
    bool inUse { get; set; }
    bool isUsable { get; set; }
    bool highlightOnTouch { get; set; }

    void Touch(bool beingTouched);
    void Interact();
}

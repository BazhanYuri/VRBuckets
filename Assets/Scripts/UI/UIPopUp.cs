namespace UI
{
    public class UIPopUp : UIComponent
    {
        public int showTime;

        public override void Show()
        {
            base.Show();
            Invoke(nameof(Hide), showTime);
        }
    }
}
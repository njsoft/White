using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using White.WPFCustomControls.Automation;

namespace White.CustomControls.WPF.Peers
{
    public class WhiteWPFButtonPeer : ButtonAutomationPeer, IValueProvider
    {
        private readonly WhitePeer whitePeer;

        public WhiteWPFButtonPeer(Button button, ICustomCommandDeserializer customCommandDeserializer) : base(button)
        {
            whitePeer = new WhitePeer(this, button, customCommandDeserializer);
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return whitePeer.GetPattern(patternInterface);
        }

        public virtual void SetValue(string command)
        {
            whitePeer.SetValue(command);
        }

        public virtual string Value
        {
            get { return whitePeer.Value; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using White.Core.UIItems;

namespace White.Core.Mappings
{
    public class ControlDictionaryItems : List<ControlDictionaryItem>
    {
        public virtual void AddWin32Primary(Type testControlType, ControlType controlType)
        {
            Add(ControlDictionaryItem.Win32Primary(testControlType, controlType));
        }

        public virtual void AddWPFPrimary(Type testControlType, ControlType controlType)
        {
            Add(ControlDictionaryItem.WPFPrimary(testControlType, controlType));
        }

        public virtual void AddWinFormPrimary(Type testControlType, ControlType controlType)
        {
            Add(ControlDictionaryItem.WinFormPrimary(testControlType, controlType));
        }

        public virtual void AddSilverlightPrimary(Type testControlType, ControlType controlType)
        {
            Add(ControlDictionaryItem.SilverlightPrimary(testControlType, controlType));
        }

        public virtual void AddPrimary(Type testControlType, ControlType controlType)
        {
            Add(ControlDictionaryItem.Primary(testControlType, controlType));
        }

        public virtual void AddSecondary(Type testControlType, ControlType controlType)
        {
            Add(ControlDictionaryItem.Secondary(testControlType, controlType));
        }

        public virtual void AddPrimary(Type testControlType, ControlType controlType, bool hasPrimaryChildren)
        {
            Add(ControlDictionaryItem.Primary(testControlType, controlType, hasPrimaryChildren));
        }

        public virtual void AddSecondary(Type testControlType, ControlType controlType, bool hasPrimaryChildren)
        {
            Add(ControlDictionaryItem.Secondary(testControlType, controlType, hasPrimaryChildren));
        }

        public virtual ControlDictionaryItem FindBy(ControlType controlType)
        {
            return Find(obj => controlType.Equals(obj.ControlType) && !obj.IsIdentifiedByClassName && !obj.IsIdentifiedByName);
        }

        public virtual ControlDictionaryItem FindBy(Type testControlType, string frameworkId)
        {
            var controlDictionaryItems = this
                .Where(controlDictionaryItem => IsSameOrFrameworkSpecificImplentation(testControlType, controlDictionaryItem))
                .ToArray();

            if (controlDictionaryItems.Length == 0)
                return null;

            if (controlDictionaryItems.Length == 1)
                return controlDictionaryItems.Single();

            var frameworkMatching = controlDictionaryItems
                .Where(c=>frameworkId == null || c.FrameworkId == frameworkId)
                .ToArray();

            if (frameworkMatching.Length > 1)
            {
                throw new WhiteException(string.Format("{0} can be mapped to {1} depending on the target framework", 
                    testControlType.Name,
                    string.Join(" or ", frameworkMatching.Select(m=>m.ControlType.LocalizedControlType))));
            }

            return frameworkMatching.SingleOrDefault();
        }

        private static bool IsSameOrFrameworkSpecificImplentation(Type testControlType, ControlDictionaryItem controlDictionaryItem)
        {
            return 
                testControlType == controlDictionaryItem.TestControlType ||
                testControlType ==  PlatformSpecificItemAttribute.BaseType(controlDictionaryItem.TestControlType);
        }

        public virtual void AddFrameworkSpecificPrimary(ControlType controlType, Type win32Type, Type winformType, Type wpfType, Type silverlightType)
        {
            AddWin32Primary(win32Type, controlType);
            AddWinFormPrimary(winformType, controlType);
            AddWPFPrimary(wpfType, controlType);
            AddSilverlightPrimary(silverlightType, controlType);
        }
    }
}
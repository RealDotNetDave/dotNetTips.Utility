    using System.Collections.Concurrent;
    using System.Collections.Generic;
namespace dotNetTips.Utility.Portable.Logger
{
    public sealed class AdditionalInformationItemCollection : BlockingCollection<AdditionalInformationItem>
    {
        public void Add(string itemProperty, string itemText)
        {
            this.Add(new AdditionalInformationItem(itemProperty, itemText));
        }

        public void AddRange(IDictionary<string, string> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item.Key, item.Value);
            }
        }

        public void AddRange(IEnumerable<AdditionalInformationItem> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public AdditionalInformationItem SelectByProperty(string property)
        {
            return null;
        }
    }
}

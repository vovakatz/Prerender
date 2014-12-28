using Huge.Prerender.Models;
using System.Configuration;

namespace Huge.Prerender.Scheduler
{
    public class SchedulerConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("settings", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SchedulerSettingCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public SchedulerSettingCollection SchedulerSettings
        {
            get
            {
                return (SchedulerSettingCollection)base["settings"];
            }
        }
    }

    public class SchedulerSettingElements : ConfigurationElement
    {
        [ConfigurationProperty("websiteKey", IsRequired = true, IsKey = true)]
        public string WebsiteKey
        {
            get
            {
                return (string)this["websiteKey"];
            }
            set
            {
                this["websiteKey"] = value;
            }
        }

        [ConfigurationProperty("serviceEndPointUrl", IsRequired = true)]
        public string ServiceEndPointUrl
        {
            get
            {
                return (string)this["serviceEndPointUrl"];
            }
            set
            {
                this["serviceEndPointUrl"] = value;
            }
        }

        [ConfigurationProperty("cronExpression", IsRequired = true)]
        public string CronExpression
        {
            get
            { 
                return (string)this["cronExpression"]; 
            }
            set
            { 
                this["cronExpression"] = value; 
            }
        }

        [ConfigurationProperty("sitemapUrl", IsRequired = true)]
        public string SitemapUrl
        {
            get
            {
                return (string)this["sitemapUrl"];
            }
            set
            {
                this["sitemapUrl"] = value;
            }
        }

        [ConfigurationProperty("storageType")]
        public Enums.StorageType StorageType
        {
            get
            {
                return (Enums.StorageType)this["storageType"];
            }
            set
            {
                this["storageType"] = value;
            }
        }

    }

    public class SchedulerSettingCollection : ConfigurationElementCollection
    {
        public SchedulerSettingElements this[int index]
        {
            get
            {
                return base.BaseGet(index) as SchedulerSettingElements;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }
        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new SchedulerSettingElements();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SchedulerSettingElements)element).SitemapUrl;
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}

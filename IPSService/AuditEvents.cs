using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;


namespace IPSService
{
	public enum AuditEventTypes
	{
		Information = 0,
		Warning = 1,
		Critical = 2
	}

	public class AuditEvents
	{
		private static ResourceManager resourceManager = null;
		private static object resourceLock = new object();

		private static ResourceManager ResourceMgr
		{
			get
			{
				lock (resourceLock)
				{
					if (resourceManager == null)
					{
						resourceManager = new ResourceManager
							(typeof(AuditEventFile).ToString(),
							Assembly.GetExecutingAssembly());
					}
					return resourceManager;
				}
			}
		}

		public static string Information
		{
			get
			{
				
				return ResourceMgr.GetString(AuditEventTypes.Information.ToString());
			}
		}

		public static string Warning
		{
			get
			{
				
				return ResourceMgr.GetString(AuditEventTypes.Warning.ToString());
			}
		}

		public static string Critical
		{
			get
			{
				
				return ResourceMgr.GetString(AuditEventTypes.Critical.ToString());
			}
		}
	}

}

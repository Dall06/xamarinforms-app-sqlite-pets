using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppXamarinPets.Extensions
{
    public static class PetsExtension
    {
		public static async void SafeFireAndForget(this Task task,
										   bool returnToCallingContext,
										   Action<Exception> onException = null)
		{
			try
			{
				await task.ConfigureAwait(returnToCallingContext);
			}
			catch (Exception ex) when (onException != null)
			{
				onException(ex);
			}
		}
	}
}

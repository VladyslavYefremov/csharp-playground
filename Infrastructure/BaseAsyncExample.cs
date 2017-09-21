using System.Threading.Tasks;

namespace Infrastructure
{
	public abstract class BaseAsyncExample : BaseExample
	{
		public abstract Task RunAsync();
	}
}
using System;

namespace Examles.Events
{
	public class Inheritance
	{
		private class Subject
		{ 
			public event EventHandler OnUpdate;

			public virtual void PublishEvent()
			{
				Update(this, null);
			}

			/**
			 * For ability to `Invoke` event from derived classes
			 * this class mush declare following member:
			 */
			protected virtual void Update(object sender, EventArgs e)
			{
				var handler = OnUpdate;

				handler?.Invoke(this, e);
			}
		}

		private class DerivedSubject : Subject
		{
			public override void PublishEvent()
			{
				/**
				 * The following (commented) lines of code cause compilation error:		
				 *		OnUpdate can only appear on the left side of -= and += operators
				 *		
				 *	The reason is that .Net creates private instance beriables behind the scenes
				 *	that actually hold the delegate.
				 *	
				 *	For example:
				 *		public event EventHandler OnUpdate;
				 *	
				 *	Is actualy is compiled into:
				 *		private EventHandler onUpdateDelegate;
				 *		
				 *		public event EventHandler OnUpdate
				 *		{
				 *			add { onUpdateDelegate += value; }
				 *			remove { onUpdateDelegate -= value; }
				 *		}
				 *		
				 *	So the private instance of delegate cannot be accessed from 
				 *	class that doesn't declare it
				 */

				//OnUpdate?.Invoke(this, null);

				/**
				 * Calling this method, declared in parent class, allows
				 * to invoke delegate
				 */
				Update(this, null);
			}
		}
	}
}
namespace ICarDMS.Proxy.PB
{
	[System.Diagnostics.DebuggerStepThrough]
	public class n_webapiproxy : System.IDisposable 
	{
		internal c__n_webapiproxy __nvo__;
		private bool ____disposed____ = false;
		public void Dispose()
		{
			if (____disposed____)
				return;
			____disposed____ = true;
			c__webapiproxy_app.InitSession(__nvo__.Session);
			Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(__nvo__);
			c__webapiproxy_app.RestoreOldSession();
		}
		public n_webapiproxy()
		{
			
			c__webapiproxy_app.InitAssembly();
			__nvo__ = (c__n_webapiproxy)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(c__n_webapiproxy));
			c__webapiproxy_app.RestoreOldSession();
		}
		internal n_webapiproxy(c__n_webapiproxy nvo)
		{
			__nvo__ = nvo;
		}
		public virtual void execute(ICarDMS.Interface.IPBProxy fromdotnet)
		{
			c__webapiproxy_app.InitSession(__nvo__.Session);
			((c__n_webapiproxy)__nvo__).execute(fromdotnet);
			c__webapiproxy_app.RestoreOldSession();
		}
	}
} 
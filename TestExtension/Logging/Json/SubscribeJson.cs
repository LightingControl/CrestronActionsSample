namespace TwoN.Logging.Json
{
	internal class SubscribeJson
	{
		public bool Success { get; set; }
		public Subscribe.Result Result { get; set; }
	}

	namespace Subscribe
	{
		internal class Result
		{
			public uint Id { get; set; }
		}
	}
}

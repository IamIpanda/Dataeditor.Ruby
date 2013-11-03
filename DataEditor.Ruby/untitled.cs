using System
using System.Text
using DataEditor.FuzzyData

namespace DataEditor.Ruby
{
	public class RubyData
	{
		public RubyData Instance { get;set; }
		static RubyData()
		{
			Instance = new RubyData();
		}
		protected RubyData() { }
		protected Dictionary<string,FuzzyObject> Datas = new Dictionary<string,FuzzyObject>();
		public FuzzyObject this[string name]
		{
			get
			{

			}			
		}
	}
}
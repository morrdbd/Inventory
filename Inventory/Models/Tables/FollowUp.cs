using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
	public class FollowUp
	{
		public int ID { get; set; }
		public int DocumentTypeId { get; set; }
		public string Subject { get; set; }
		public string Instruction { get; set; }
		public int OrderNumber { get; set; }
		public DateTime OrderDate { get; set; }
		public int ImplementerId { get; set; }
		public string OrderBy { get; set; }
		public string Details { get; set; }
	}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetA.Client.Model
{
	public class AppUser
	{
		public string PhoneNumber { get; set; }
		public string NormalizedEmail { get; set; }
		public string Email { get; set; }
		public string NormalizedUserName { get; }
		public string UserName { get; set; }
	}
}
//End of Code
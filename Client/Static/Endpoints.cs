﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetA.Client.Static
{
	public static class Endpoints
	{
		private static readonly string Prefix = "api";

		//i am pretty sure you know what to do or you are hopeless.
		public static readonly string ExpensesEndpoint = $"{Prefix}/expensess";
		public static readonly string PaymentMethodsEndpoint = $"{Prefix}/paymentmethods";
		public static readonly string AccountsEndpoint = $"{Prefix}/accounts";
	}
}

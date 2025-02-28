﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Customer;

public class CustomerDto
{
	public int Id { get; set; }

	public string UserName { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string EmailAddress { get; set; }
}

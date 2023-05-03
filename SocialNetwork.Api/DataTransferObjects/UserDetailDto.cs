﻿using System;
using SocialNetwork.Api.Models;

namespace SocialNetwork.Api.DataTransferObjects
{
	public class UserDetailDto
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public ICollection<PostDetailDto> Posts { get; set; }
    }
}


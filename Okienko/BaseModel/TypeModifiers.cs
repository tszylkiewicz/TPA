﻿using BaseModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class TypeModifiers
    {
        public AbstractEnum? AbstractEnum { get; set; }
        public AccessLevel? AccessLevel { get; set; }
        public SealedEnum? SealedEnum { get; set; }
        public StaticEnum? StaticEnum { get; set; }
    }
}

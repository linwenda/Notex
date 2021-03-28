﻿namespace Funzone.BuildingBlocks.Domain
{
    //Base on http://www.kamilgrzybek.com/design/domain-model-validation/
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}
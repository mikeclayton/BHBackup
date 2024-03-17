﻿namespace BHBackup.Client.GraphQl.Identity.Models;

internal interface IPerson
{

    public string TypeName
    {
        get;
    }

    public Name Name
    {
        get;
    }

    public ProfileImage? ProfileImage
    {
        get;
    }

}
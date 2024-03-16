﻿using BHDownload.Client.GraphQl.Observations.Models;

namespace BHDownload.Visitors;

internal abstract partial class RepositoryVisitor
{

    public virtual void Visit(IEnumerable<Observation> observations)
    {
        foreach (var observation in observations)
        {
            this.Visit(observation);
        }
    }

    public virtual void Visit(Observation observation)
    {
        this.Visit(observation.CreatedBy);
    }

    public virtual void Visit(ObservationPerson person)
    {
    }

}

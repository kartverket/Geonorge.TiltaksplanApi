﻿using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class ActionPlanConfiguration
    {
        internal static void Configure(EntityTypeBuilder<ActionPlan> builder)
        {
            builder
                .ToTable("ActionPlans")
                .HasKey(actionPlan => actionPlan.Id);

            builder
                .Property(actionPlan => actionPlan.Name)
                .IsRequired();

            builder
                .Property(actionPlan => actionPlan.Progress)
                .IsRequired();

            builder
                .Property(actionPlan => actionPlan.Volume)
                .IsRequired();

            builder
                .Property(actionPlan => actionPlan.Status)
                .IsRequired();

            builder
                .Property(actionPlan => actionPlan.TrafficLight)
                .IsRequired();

            builder
                .HasMany(actionPlan => actionPlan.Activities)
                .WithOne()
                .HasForeignKey(activity => activity.ActionPlanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(actionPlan => actionPlan.Id)
                .ValueGeneratedOnAdd();
        }
    }
}

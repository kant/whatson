﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CruiseControlStatusViewModel.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
//   Licensed under the MIT License. See License-file in the project root for license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.CruiseControl.GUI
{
  using System;
  using Soloplan.WhatsON.GUI.Common.BuildServer;
  using Soloplan.WhatsON.GUI.Common.ConnectorTreeView;
  using Soloplan.WhatsON.Model;

  public class CruiseControlStatusViewModel : BuildStatusViewModel
  {
    /// <summary>
    /// The backing field for <see cref="BuildTimeUnknown"/>.
    /// </summary>
    private bool buildTimeUnknown;

    public CruiseControlStatusViewModel(ConnectorViewModel model)
      : base(model)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether estimated build time is known.
    /// </summary>
    public bool BuildTimeUnknown
    {
      get => this.buildTimeUnknown;
      set
      {
        if (this.buildTimeUnknown != value)
        {
          this.buildTimeUnknown = value;
          this.OnPropertyChanged();
        }
      }
    }

    public override void Update(Status newStatus)
    {
      base.Update(newStatus);
      var ccStatus = newStatus as CruiseControlStatus;
      if (ccStatus == null)
      {
        return;
      }

      if (this.State == ObservationState.Running && this.EstimatedDuration.TotalSeconds > 0)
      {
        var elapsedSinceStart = (DateTime.Now - ccStatus.NextBuildTime).TotalSeconds;
        this.RawProgress = (int)((100 * elapsedSinceStart) / this.EstimatedDuration.TotalSeconds);
      }
      else
      {
        this.RawProgress = 0;
      }

      this.Culprits.Clear();
      foreach (var culprit in ccStatus.Culprits)
      {
        var culpritModel = new UserViewModel();
        culpritModel.FullName = culprit.Name;
        this.Culprits.Add(culpritModel);
      }

      this.UpdateCalculatedFields();

      if (this.State == ObservationState.Running && this.EstimatedDuration.TotalSeconds < 1)
      {
        this.BuildingLongerThenExpected = false;
        this.BuildingNoLongerThenExpected = false;
        this.BuildTimeUnknown = true;
      }
      else
      {
        this.BuildTimeUnknown = false;
      }
    }
  }
}
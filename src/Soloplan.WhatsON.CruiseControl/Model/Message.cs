﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
//   Licensed under the MIT License. See License-file in the project root for license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.CruiseControl.Model
{
  using System.Xml.Serialization;

  [System.SerializableAttribute]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public class Message
  {
    [XmlAttributeAttribute("text")]
    public string Text { get; set; }

    [XmlAttributeAttribute("kind")]
    public string Kind { get; set; }
  }
}
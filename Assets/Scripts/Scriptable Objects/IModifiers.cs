﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifiers {
  void AddValue(ref int baseValue);
  void AddValue(ref float baseValue);
  void RemoveValue(ref int baseValue);
  void RemoveValue(ref float baseValue);
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece {
  bool IsTraversable();
  bool CanConstructOn();
  bool IsDamagable();
}

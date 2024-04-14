using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletPatterns {
    public class NormalBulletPattern : MonoBehaviour {
        public NormalBulletPattern(BulletProperty bullet) {
            bullet.SetVelocity(Vector2.down * bullet.speed);
        }
    }
}


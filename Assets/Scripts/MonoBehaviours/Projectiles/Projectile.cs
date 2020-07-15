using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public static void Create(Vector3 spawnPosition, GameboardPiece piece, int damageAmount) {
      Transform projectileTransform = Instantiate(ProjectileAssets.Instance.baseRailbowProjectile, spawnPosition, Quaternion.identity);
      Projectile projectile = projectileTransform.GetComponent<Projectile>();
      projectile.Setup(piece, damageAmount);
    }

    private GameboardPiece piece;
    private int damageAmount;
    private Vector3 lastPos;
    // Start is called before the first frame update
    void Setup(GameboardPiece gp, int damageAmount) {
      this.piece = gp;
      this.damageAmount = damageAmount;
      this.lastPos = gp.GetPosition();
    }

    void Update() {
      Vector3 targetPos;

      if (piece != null) {
        targetPos = lastPos =  piece.GetPosition();
      } else {
        targetPos = lastPos;
      }

      Vector3 moveDir = (targetPos - transform.position).normalized;

      float moveSpeed = 4f;
      transform.position += moveDir * moveSpeed * Time.deltaTime;

      float angle = Mathf.Atan2(moveDir.x, moveDir.y); // Fix rotation
      transform.eulerAngles = new Vector3(0, 0, angle);

      if (Vector3.Distance(transform.position, targetPos) < 0.05f) {
        if (piece != null) piece.TakeDamage(damageAmount);
        Destroy(gameObject);
      }
    }
}

using UnityEngine;

namespace Models
{
    public class SpriteRendererSize : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private bool _isColliderInFullSprite;

        private void Start()
        {
            if (_isColliderInFullSprite)
            {
                _collider.size = _spriteRenderer.sprite.bounds.size;
                _collider.offset = new Vector2(0, 0);
            }
            else
            {
                _collider.offset = new Vector2(0, -_spriteRenderer.bounds.size.y * 1.75f);
                _collider.size = new Vector2(_spriteRenderer.sprite.bounds.size.x, _collider.size.y) ;
            }
        }
    }
}
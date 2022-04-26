using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandomizer : MonoBehaviour
{
    [System.Serializable]
    public class RoomTexture
    {
        public GameObject roomVariant;
        public Direction doorDirection;
    }

    public RoomTexture[] spriteToChoose;

    public bool isRoomSquare = true;

    private GameObject roomSpriteObject;

    // Start is called before the first frame update
    void Awake()
    {
        //Stop editor when no sprites has been assigned.
        if (spriteToChoose.Length == 0)
        {
            Debug.LogWarning("TextureRandomizer: Textures has not been filled in!");
            Debug.Break();
        }
    }

    public void SetSpriteOrientation(List<Direction> directions)
    {
        if (directions.Count == 0)
        {
            //No direction found, building default direction of the sprite
            AppendTextureToRoom(spriteToChoose[0]);
            return;
        }

        Direction directionToRotate = directions[Random.Range(0, directions.Count)];

        if (isRoomSquare)
        {
            SetSpriteRotation(directionToRotate);
        }
        else
        {
            SetSpriteVersion(directionToRotate);
        }
    }

    private void SetSpriteRotation(Direction direction)
    {
        RoomTexture currentSprite = spriteToChoose[Random.Range(0, spriteToChoose.Length)];
        AppendTextureToRoom(currentSprite);

        if (currentSprite.doorDirection == direction)
        {
            return;
        }
        else
        {
            int rotationAmount = (int)direction - (int)currentSprite.doorDirection;
            roomSpriteObject.transform.Rotate(new Vector3(0, 0, -90 * rotationAmount));
        }
    }

    private void SetSpriteVersion(Direction direction)
    {
        List<RoomTexture> roomSprites = new List<RoomTexture>();

        foreach (RoomTexture roomSprite in spriteToChoose)
        {
            if (roomSprite.doorDirection == direction)
            {
                roomSprites.Add(roomSprite);
            }
        }

        if (roomSprites.Count == 0)
        {
            AppendTextureToRoom(spriteToChoose[0]);
            return;
        }

        RoomTexture currentSprite = roomSprites[Random.Range(0, spriteToChoose.Length)];
        AppendTextureToRoom(currentSprite);
    }

    private void AppendTextureToRoom(RoomTexture sprite)
    {
        roomSpriteObject = Instantiate(sprite.roomVariant, gameObject.transform);

        roomSpriteObject.transform.parent = gameObject.transform;

        roomSpriteObject.name = "Room Texture";
    }
}

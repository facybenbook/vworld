using UnityEngine;
using System.Collections;

public static class EditTerrain {

	//make WorldPos from a vector3
	public static WorldPos GetBlockPos(Vector3 pos) {
		WorldPos blockPos = new WorldPos(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
		return blockPos;
	}

	public static WorldPos GetBlockPos(RaycastHit hit, bool adjacent = false) {
		//for future reference this may only work with solid cube collision meshes, or at least work as intended
		Vector3 pos = new Vector3(MoveWithinBlock(hit.point.x, hit.normal.x, adjacent), MoveWithinBlock(hit.point.y, hit.normal.y, adjacent), MoveWithinBlock(hit.point.z, hit.normal.z, adjacent));
		return GetBlockPos(pos);
	}

	//move into or away from hit point so that point is actually within a block and not in between two
	static float MoveWithinBlock(float pos, float norm, bool adjacent = false) {
		if(pos - (int)pos == 0.5f || pos - (int)pos == -0.5f) {
			if(adjacent)
				pos += norm/10;//divide by a bunch just to be sure we dont go too far
			else
				pos -= norm/10;
		}
		return (float)pos;
	}

	public static bool SetBlock(RaycastHit hit, Block block, bool adjacent = false) {
		Chunk chunk = hit.collider.GetComponent<ChunkContainer>().chunk;
		if(chunk == null)
			return false;

		WorldPos pos = GetBlockPos(hit, adjacent);
		chunk.world.SetBlock(pos.x, pos.y, pos.z, block);
		return true;
	}

	public static Block GetBlock(RaycastHit hit, bool adjacent = false) {
		Chunk chunk = hit.collider.GetComponent<Chunk>();
		if(chunk == null)
			return null;
		
		WorldPos pos = GetBlockPos(hit, adjacent);
		Block block = chunk.world.GetBlock(pos.x, pos.y, pos.z);
		return block;
	}
}

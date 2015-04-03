﻿using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockAir : Block {
	public BlockAir() : base() {

	}

	public override MeshData BlockData (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.useRenderDataForCollision = false;
		//air will connect terrain blocks next to it
		bool northSolid = chunk.GetBlock(x, y, z+1).IsSolid(Direction.south); 
		bool southSolid = chunk.GetBlock(x, y, z-1).IsSolid(Direction.north);
		bool eastSolid = chunk.GetBlock(x+1, y, z).IsSolid(Direction.west);
		bool westSolid = chunk.GetBlock(x-1, y, z).IsSolid(Direction.east);
		bool upSolid = chunk.GetBlock(x, y+1, z).IsSolid(Direction.down);
		bool downSolid = chunk.GetBlock(x, y-1, z).IsSolid(Direction.up);

		bool northTer = chunk.GetBlock(x, y, z+1).isTerrain; 
		bool southTer = chunk.GetBlock(x, y, z-1).isTerrain;
		bool eastTer = chunk.GetBlock(x+1, y, z).isTerrain;
		bool westTer = chunk.GetBlock(x-1, y, z).isTerrain;
		bool upTer = chunk.GetBlock(x, y+1, z).isTerrain;
		bool downTer = chunk.GetBlock(x, y-1, z).isTerrain;

		int numTer = 0;
		if(northTer)
			numTer++;
		if(southTer)
			numTer++;
		if(eastTer)
			numTer++;
		if(westTer)
			numTer++;
		if(upTer)
			numTer++;
		if(downTer)
			numTer++;

		if(numTer == 2) {
			if(northTer) {																																				//case 1
				if(downTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {9, 11, 18, 19}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(upTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {11, 17, 16, 9}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(eastTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 17, 18, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(westTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 10, 19, 16}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				}
			} else if(southTer) {
				if(downTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {13, 15, 19, 18}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(upTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {15, 16, 17, 13}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(eastTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {12, 14, 18, 17}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(westTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {12, 16, 19, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				}
			} else if(downTer) {
				if(eastTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {11, 13, 14, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(westTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {15, 9, 10, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				}
			} else if(upTer) {
				if(eastTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 11, 13, 12}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(westTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 9, 15, 16}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				}
			}
		}
		return meshData;
	}

	public override bool IsSolid (Direction direction) {
		return false;//not solid on all sides
	}

	public override Tile GetTexturePosition (Direction direction, Chunk chunk, int x, int y, int z) {
		if(chunk == null) 
			return base.GetTexturePosition(direction);

		if(chunk.GetBlock(x, y-1, z).isTerrain) {
			return chunk.GetBlock(x, y-1, z).GetTexturePosition(Direction.up);
		} else if(chunk.GetBlock(x, y+1, z).isTerrain) {
			return chunk.GetBlock(x, y+1, z).GetTexturePosition(Direction.up);
		} else if(chunk.GetBlock(x, y, z+1).isTerrain) {
			return chunk.GetBlock(x, y, z+1).GetTexturePosition(Direction.up);
		} else if(chunk.GetBlock(x, y, z-1).isTerrain) {
			return chunk.GetBlock(x, y, z-1).GetTexturePosition(Direction.up);
		} else if(chunk.GetBlock(x+1, y, z).isTerrain) {
			return chunk.GetBlock(x+1, y, z).GetTexturePosition(Direction.up);
		} else if(chunk.GetBlock(x-1, y, z).isTerrain) {
			return chunk.GetBlock(x-1, y, z).GetTexturePosition(Direction.up);
		} else {
			return base.GetTexturePosition(direction);
		}
	}
}

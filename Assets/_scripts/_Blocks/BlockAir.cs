﻿using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockAir : Block {

	public BlockAir() : base() {
		
	}

	public override MeshData BlockData (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.useRenderDataForCollision = true;
		//air will connect terrain blocks next to it
		bool northSolid = chunk.GetBlock(x, y, z+1).IsSolid(Direction.south); 
		bool southSolid = chunk.GetBlock(x, y, z-1).IsSolid(Direction.north);
		bool eastSolid = chunk.GetBlock(x+1, y, z).IsSolid(Direction.west);
		bool westSolid = chunk.GetBlock(x-1, y, z).IsSolid(Direction.east);
//		bool upSolid = chunk.GetBlock(x, y+1, z).IsSolid(Direction.down);
//		bool downSolid = chunk.GetBlock(x, y-1, z).IsSolid(Direction.up);

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

		//note that they are not if else, just if, so that multiple cases are all handled in this long expression
		//the boolean expressions for the side tris could be simplified to use one less chunk.getblock call (instead of !((A && B) || (C && B)), make it (!B || (!A && !C))
		//however, it would be a lot of rewriting things and the performance increase would be negligable I'd assume
		if(numTer > 1 && numTer < 5) {
			if(northTer) {																													//four sub cases removed cause they were ugly
				if(downTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {19, 9, 11, 18}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					if(!eastSolid && !((chunk.GetBlock(x+1, y-1, z+1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain) || (chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {3, 18, 11}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
					if(!westSolid && !((chunk.GetBlock(x-1, y-1, z+1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain) || (chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {2, 9, 19}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
				} if(upTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {9, 16, 17, 11}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					if(!eastSolid && !((chunk.GetBlock(x+1, y+1, z+1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain) || (chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {0, 11, 17}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
					if(!westSolid && !((chunk.GetBlock(x-1, y+1, z+1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain) || (chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {1, 16, 9}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
				} /*else if(eastTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 17, 18, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(westTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 10, 19, 16}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				}*/
			} if(southTer) {
				if(downTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {18, 13, 15, 19}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					if(!eastSolid && !((chunk.GetBlock(x+1, y-1, z-1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain) || (chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {6, 13, 18}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
					if(!westSolid && !((chunk.GetBlock(x-1, y-1, z-1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain) || (chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {7, 19, 15}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
				} if(upTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {13, 17, 16, 15}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					if(!eastSolid && !((chunk.GetBlock(x+1, y+1, z-1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain) || (chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {5, 17, 13}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
					if(!westSolid && !((chunk.GetBlock(x-1, y+1, z-1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain) || (chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {4, 15, 16}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
				} /*else if(eastTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {12, 14, 18, 17}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				} else if(westTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {12, 16, 19, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
				}*/
			} if(downTer) {
				if(eastTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {11, 13, 14, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					if(!northSolid && !((chunk.GetBlock(x+1, y-1, z+1).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain) || (chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {11, 10, 3}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);//
					}
					if(!southSolid && !((chunk.GetBlock(x+1, y-1, z-1).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain) || (chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {14, 13, 6}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
				} if(westTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {15, 9, 10, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					if(!northSolid && !((chunk.GetBlock(x-1, y-1, z+1).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain) || (chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {10, 9, 2}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
					if(!southSolid && !((chunk.GetBlock(x-1, y-1, z-1).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain) || (chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {7, 15, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
				}
			} if(upTer) {
				if(eastTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {11, 8, 12, 13}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					if(!northSolid && !((chunk.GetBlock(x+1, y+1, z+1).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain) || (chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {0, 8, 11}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
					if(!southSolid && !((chunk.GetBlock(x+1, y+1, z-1).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain) || (chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {5, 13, 12}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
				} if(westTer) {
					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {15, 12, 8, 9}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					if(!northSolid && !((chunk.GetBlock(x-1, y+1, z+1).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain) || (chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {1, 9, 8}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
					if(!southSolid && !((chunk.GetBlock(x-1, y+1, z-1).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain) || (chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain))) {
						meshData = AddFaceTri(chunk, x, y, z, new int[3] {4, 12, 15}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
					}
				}
			}
		}
		if(downTer) {
			if(!westSolid && !northSolid && chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain) {
				meshData = AddFaceTri(chunk, x, y, z, new int[3] {19, 9, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
			}
			if(!eastSolid && !northSolid && chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain) {
				meshData = AddFaceTri(chunk, x, y, z, new int[3] {11, 18, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
			}
			if(!eastSolid && !southSolid && chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain) {
				meshData = AddFaceTri(chunk, x, y, z, new int[3] {13, 14, 18}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
			}
			if(!westSolid && !southSolid && chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain) {
				meshData = AddFaceTri(chunk, x, y, z, new int[3] {15, 19, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
			}
		}
		if(upTer) {
			if(!westSolid && !northSolid && chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain) {
				meshData = AddFaceTri(chunk, x, y, z, new int[3] {16, 8, 9}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
			}
			if(!eastSolid && !northSolid && chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain) {
				meshData = AddFaceTri(chunk, x, y, z, new int[3] {17, 11, 8}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
			}
			if(!eastSolid && !southSolid && chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain) {
				meshData = AddFaceTri(chunk, x, y, z, new int[3] {17, 12, 13}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
			}
			if(!westSolid && !southSolid && chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain) {
				meshData = AddFaceTri(chunk, x, y, z, new int[3] {12, 16, 15}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
			}
		}

//		if(numTer > 1 && numTer < 5) {
//			if(northTer) {																													//four sub cases removed cause they were ugly
//				if(downTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {1, 0, 18, 19}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					if(!eastSolid && !((chunk.GetBlock(x+1, y-1, z+1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain) || (chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {0, 3, 18}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//					if(!westSolid && !((chunk.GetBlock(x-1, y-1, z+1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain) || (chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {1, 19, 2}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//				} if(upTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {16, 17, 3, 2}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					if(!eastSolid && !((chunk.GetBlock(x+1, y+1, z+1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain) || (chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {17, 0, 3}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//					if(!westSolid && !((chunk.GetBlock(x-1, y+1, z+1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain) || (chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {1, 16, 2}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//				} /*else if(eastTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 17, 18, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//				} else if(westTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 10, 19, 16}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//				}*/
//			} if(southTer) {
//				if(downTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {5, 4, 19, 18}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					if(!eastSolid && !((chunk.GetBlock(x+1, y-1, z-1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain) || (chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {5, 18, 6}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//					if(!westSolid && !((chunk.GetBlock(x-1, y-1, z-1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain) || (chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {4, 7, 19}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//				} if(upTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {17, 16, 7, 6}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					if(!eastSolid && !((chunk.GetBlock(x+1, y+1, z-1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain) || (chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {6, 5, 17}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//					if(!westSolid && !((chunk.GetBlock(x-1, y+1, z-1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain) || (chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {16, 4, 7}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//				} /*else if(eastTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {12, 14, 18, 17}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//				} else if(westTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {12, 16, 19, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//				}*/
//			} if(downTer) {
//				if(eastTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {0, 5, 14, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					if(!northSolid && !((chunk.GetBlock(x+1, y-1, z+1).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain) || (chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {0, 10, 3}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);//
//					}
//					if(!southSolid && !((chunk.GetBlock(x+1, y-1, z-1).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain) || (chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {14, 5, 6}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//				} if(westTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {4, 1, 10, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					if(!northSolid && !((chunk.GetBlock(x-1, y-1, z+1).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain) || (chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {10, 1, 2}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//					if(!southSolid && !((chunk.GetBlock(x-1, y-1, z-1).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain) || (chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {7, 4, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//				}
//			} if(upTer) {
//				if(eastTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {8, 12, 6, 3}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					if(!northSolid && !((chunk.GetBlock(x+1, y+1, z+1).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain) || (chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {0, 8, 3}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//					if(!southSolid && !((chunk.GetBlock(x+1, y+1, z-1).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain) || (chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {12, 5, 6}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//				} if(westTer) {
//					meshData = AddFaceQuad(chunk, x, y, z, new int[4] {12, 8, 2, 7}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					if(!northSolid && !((chunk.GetBlock(x-1, y+1, z+1).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain) || (chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {8, 1, 2}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//					if(!southSolid && !((chunk.GetBlock(x-1, y+1, z-1).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain) || (chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain))) {
//						meshData = AddFaceTri(chunk, x, y, z, new int[3] {7, 4, 12}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//					}
//				}
//			}
//		}
//		if(downTer) {
//			if(!westSolid && !northSolid && chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain) {
//				meshData = AddFaceTri(chunk, x, y, z, new int[3] {19, 1, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//			}
//			if(!eastSolid && !northSolid && chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain && chunk.GetBlock(x, y-1, z+1).isTerrain) {
//				meshData = AddFaceTri(chunk, x, y, z, new int[3] {0, 18, 10}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//			}
//			if(!eastSolid && !southSolid && chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x+1, y-1, z).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain) {
//				meshData = AddFaceTri(chunk, x, y, z, new int[3] {5, 14, 18}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//			}
//			if(!westSolid && !southSolid && chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x-1, y-1, z).isTerrain && chunk.GetBlock(x, y-1, z-1).isTerrain) {
//				meshData = AddFaceTri(chunk, x, y, z, new int[3] {4, 19, 14}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//			}
//		}
//		if(upTer) {
//			if(!westSolid && !northSolid && chunk.GetBlock(x-1, y, z+1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain) {
//				meshData = AddFaceTri(chunk, x, y, z, new int[3] {16, 8, 2}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//			}
//			if(!eastSolid && !northSolid && chunk.GetBlock(x+1, y, z+1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain && chunk.GetBlock(x, y+1, z+1).isTerrain) {
//				meshData = AddFaceTri(chunk, x, y, z, new int[3] {17, 3, 8}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//			}
//			if(!eastSolid && !southSolid && chunk.GetBlock(x+1, y, z-1).isTerrain && chunk.GetBlock(x+1, y+1, z).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain) {
//				meshData = AddFaceTri(chunk, x, y, z, new int[3] {17, 12, 6}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//			}
//			if(!westSolid && !southSolid && chunk.GetBlock(x-1, y, z-1).isTerrain && chunk.GetBlock(x-1, y+1, z).isTerrain && chunk.GetBlock(x, y+1, z-1).isTerrain) {
//				meshData = AddFaceTri(chunk, x, y, z, new int[3] {16, 12, 7}, GetTexturePosition(Direction.up, chunk, x, y, z), meshData);
//			}
//		}
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
		} else {
			return base.GetTexturePosition(direction);
		}
	}
}

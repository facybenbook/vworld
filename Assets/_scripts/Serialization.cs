﻿using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Serialization : MonoBehaviour {
	public static string saveFolderName = "vworldSaves";

	public static string SaveLocation(string worldName) {
		string saveLocation = saveFolderName + "/" + worldName + "/";

		if(!Directory.Exists(saveLocation)) {
			Directory.CreateDirectory(saveLocation);
		}
		return saveLocation;
	}

	public static string FileName(WorldPos chunkLoc) {
		string fileName = chunkLoc.x + "," + chunkLoc.y + "," + chunkLoc.z + ".bin";
		return fileName;
	}

	public static void SaveChunk(Chunk chunk) {
		Save save = new Save(chunk);
		if(save.blocks.Count == 0)
			return;

		string saveFile = SaveLocation(chunk.world.worldName);
		saveFile += FileName(chunk.pos);

		IFormatter form = new BinaryFormatter();
		Stream stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
		form.Serialize(stream, save);
		stream.Close();
	}

	public static bool Load(Chunk chunk) {
		string saveFile = SaveLocation(chunk.world.worldName);
		saveFile += FileName(chunk.pos);

		if(!File.Exists(saveFile))
			return false;

		IFormatter form = new BinaryFormatter();
		FileStream stream = new FileStream(saveFile, FileMode.Open);

		Save save = (Save)form.Deserialize(stream);
		foreach(var block in save.blocks) {
			chunk.blocks[block.Key.x, block.Key.y, block.Key.z] = block.Value;
		}

		stream.Close();
		return true;
	}
}

// ============================================================================
// AUTO GENERATED - DO NOT MODIFY
// Generated at: 2025-08-04 16:52:55
// ============================================================================

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CryStar.Story.Data;

/// <summary>
/// ストーリーシーン情報の定数クラス
/// </summary>
public static class MasterStoryScene
{
    private static readonly Dictionary<int, StorySceneData> _sceneData = new Dictionary<int, StorySceneData>
    {
        {
            1, new StorySceneData(1, "OP", 1, 1, 1, 
                "A3:O83", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },
        {
            2, new StorySceneData(2, "Loop1-Episode1to3", 2, 1, 1, 
                "A84:O255", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },
        {
            3, new StorySceneData(3, "Loop1-MorningEvent", 2, 1, 2, 
                "A257:O263", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            4, new StorySceneData(4, "Loop1-Episode4", 3, 1, 1, 
                "A265:O268", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            5, new StorySceneData(5, "Loop1-Episode5", 3, 1, 1, 
                "A270:O278", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            6, new StorySceneData(6, "Loop1-Episode6", 3, 1, 1, 
                "A280:O286", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            7, new StorySceneData(7, "Loop1-Episode7", 3, 1, 1, 
                "A288:O298", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            8, new StorySceneData(8, "Loop1-Episode8", 3, 1, 1, 
                "A300:O306", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            9, new StorySceneData(9, "Loop1-Episode9", 3, 1, 1, 
                "A308:O314", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            10, new StorySceneData(10, "Loop1-Episode10", 3, 1, 1, 
                "A316:O324", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            11, new StorySceneData(11, "Loop1-Episode11-LunchEvent", 3, 1, 1, 
                "A326:O352", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            12, new StorySceneData(12, "Loop1-Episode12-LunchEvent", 3, 1, 1, 
                "A354:O365", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            13, new StorySceneData(13, "Loop1-Episode13", 3, 1, 1, 
                "A367:O378", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            14, new StorySceneData(14, "Loop1-Episode14", 3, 1, 1, 
                "A388:O397", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            15, new StorySceneData(15, "Loop1-Episode15", 3, 1, 1, 
                "A417:O431", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            16, new StorySceneData(16, "Loop1-Episode16", 3, 1, 1, 
                "A433:O450", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            17, new StorySceneData(17, "Loop1-Episode17", 3, 1, 1, 
                "A452:O462", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            18, new StorySceneData(18, "Loop1-Episode18", 3, 1, 1, 
                "A464:O482", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            19, new StorySceneData(19, "Loop1-Episode19", 3, 1, 1, 
                "A484:O499", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            20, new StorySceneData(20, "Loop1-Episode20", 3, 1, 1, 
                "A501:O520", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            21, new StorySceneData(21, "Loop1-Episode21", 3, 1, 1, 
                "A522:O531", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            1001, new StorySceneData(1001, "Common-MorningEvent", 3, 1, 1, 
                "A406:O415", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            1002, new StorySceneData(1002, "Common-EveningEvent", 3, 1, 1, 
                "A380:O386", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },{
            1003, new StorySceneData(1003, "Common-FinishDayEvent", 3, 1, 1, 
                "A399:O404", 1.0f, new Vector3(0.0f, 0.0f, 0.0f), null
            )
        },
    };

    /// <summary>
    /// IDからストーリーシーンデータを取得
    /// </summary>
    public static StorySceneData GetSceneById(int id)
    {
        return _sceneData.GetValueOrDefault(id, null);
    }

    /// <summary>
    /// シーン名からストーリーシーンデータを取得
    /// </summary>
    public static StorySceneData GetSceneByName(string sceneName)
    {
        return _sceneData.Values.FirstOrDefault(scene => scene.SceneName == sceneName);
    }

    /// <summary>
    /// チャプターIDとシーンIDからストーリーシーンデータを取得
    /// </summary>
    public static StorySceneData GetSceneById(int chapterId, int sceneId)
    {
        return _sceneData.Values.FirstOrDefault(scene => 
            scene.ChapterId == chapterId && scene.SceneId == sceneId);
    }

    /// <summary>
    /// 指定チャプターの全シーンを取得
    /// </summary>
    public static IEnumerable<StorySceneData> GetScenesByChapter(int chapterId)
    {
        return _sceneData.Values.Where(scene => scene.ChapterId == chapterId)
                                .OrderBy(scene => scene.SceneId);
    }

    /// <summary>
    /// 指定パートの全シーンを取得
    /// </summary>
    public static IEnumerable<StorySceneData> GetScenesByPart(int partId)
    {
        return _sceneData.Values.Where(scene => scene.PartId == partId)
                                .OrderBy(scene => scene.ChapterId)
                                .ThenBy(scene => scene.SceneId);
    }

    /// <summary>
    /// 前提ストーリーが指定されているシーンを取得
    /// </summary>
    public static IEnumerable<StorySceneData> GetScenesWithPrerequisite(int prerequisiteStoryId)
    {
        return _sceneData.Values.Where(scene => scene.PrerequisiteStoryId == prerequisiteStoryId);
    }

    /// <summary>
    /// 全ストーリーシーンのIDリストを取得
    /// </summary>
    public static IEnumerable<int> GetAllSceneIds()
    {
        return _sceneData.Keys;
    }

    /// <summary>
    /// 全ストーリーシーンデータを取得
    /// </summary>
    public static IEnumerable<StorySceneData> GetAllScenes()
    {
        return _sceneData.Values.OrderBy(scene => scene.Id);
    }

    /// <summary>
    /// シーンが実行可能かチェック（前提ストーリーの条件確認）
    /// </summary>
    public static bool CanExecuteScene(int sceneId, HashSet<int> completedStories)
    {
        var scene = GetSceneById(sceneId);
        if (scene == null) return false;

        // 前提ストーリーが指定されていない場合は実行可能
        if (!scene.PrerequisiteStoryId.HasValue) return true;

        // 前提ストーリーが完了している場合は実行可能
        return completedStories.Contains(scene.PrerequisiteStoryId.Value);
    }
}


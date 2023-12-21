using Layout;

public static class UtilWorlds 
{
    public static int FindMapIndex(World world, Map map)
    {
        return world.MapsConfiguration.IndexOf(map);
    }

    public static int FindLevelIndex(Map map, LevelDefinition levelDefinition)
    {
        return map.LevelConfiguration.IndexOf(levelDefinition);
    }

    public static (LevelDefinition level, Map map) FindNextLevel(World world, Map map, LevelDefinition levelDefinition)
    {
        if (world == null || map == null || levelDefinition == null) return (null, null);
        
        var mapIndex = FindMapIndex(world, map);
        var levelIndex = FindLevelIndex(map, levelDefinition);

        if (levelIndex < map.LevelConfiguration.Count - 1)
        {
            return (map.LevelConfiguration[levelIndex + 1], map);
        }

        if (mapIndex < world.MapsConfiguration.Count - 1)
        {
            var nextMap = world.MapsConfiguration[mapIndex + 1];
            return (nextMap.LevelConfiguration[0], nextMap);
        }

        return (null, null);
    }
        
}
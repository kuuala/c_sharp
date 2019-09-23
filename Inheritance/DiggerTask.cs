using System;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
            => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
            => true;

        public int GetDrawingPriority()
            => (int)Character.Terrain;

        public string GetImageFileName()
            => "Terrain.png";
    }


    class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            int dX = 0, dY = 0;
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Up:
                    dY = -1;
                    break;
                case System.Windows.Forms.Keys.Right:
                    dX = 1;
                    break;
                case System.Windows.Forms.Keys.Down:
                    dY = 1;
                    break;
                case System.Windows.Forms.Keys.Left:
                    dX = -1;
                    break;
            }
            var newX = x + dX;
            var newY = y + dY;
            if (Tool.InMap(newX, newY) && !Tool.IsACharacter(newX, newY, Character.Sack))
                return new CreatureCommand() { DeltaX = dX, DeltaY = dY };
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
            => conflictedObject.GetDrawingPriority() == (int)Character.Sack
            || conflictedObject.GetDrawingPriority() == (int)Character.Monster;

        public int GetDrawingPriority()
            => (int)Character.Player;

        public string GetImageFileName()
            => "Digger.png";
    }

    class Sack : ICreature
    {
        private int cellCount = 0;
        
        public CreatureCommand Act(int x, int y)
        {
            var newY = y + 1;
            if (Tool.InMap(x, newY) && Tool.IsMonsterOrPlayer(x, newY) && cellCount == 0)
                return new CreatureCommand();
            if (Tool.InMap(x, newY) && (Tool.IsNothing(x, newY) || Tool.IsMonsterOrPlayer(x, newY)))
            {
                ++cellCount;
                return new CreatureCommand() { DeltaY = 1 };
            }
            if (!Tool.InMap(x, newY) || !Tool.IsNothing(x, newY))
                if (cellCount > 1)
                    return new CreatureCommand() { TransformTo = new Gold() };
                else
                    cellCount = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
            => false;

        public int GetDrawingPriority()
            => (int)Character.Sack;

        public string GetImageFileName()
            => "Sack.png";
    }

    class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
            => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetDrawingPriority() == (int)Character.Player)
                Game.Scores += 10;
            return true;
        }

        public int GetDrawingPriority()
            => (int)Character.Gold;

        public string GetImageFileName()
            => "Gold.png";
    }

    class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var playerCoord = GetPlayerCoord();
            if (playerCoord == null)
                return new CreatureCommand();
            var deltaX = playerCoord[0] - x;
            var deltaY = playerCoord[1] - y;
            var signX = Math.Sign(deltaX);
            var signY = Math.Sign(deltaY);
            var newX = x + 1 * signX;
            var newY = y + 1 * signY;
            if (Tool.InMap(newX, y) && deltaX != 0 && CanGo(newX, y))
                return new CreatureCommand() { DeltaX = signX };
            if (Tool.InMap(x, newY) && deltaY != 0 && CanGo(x, newY))
                return new CreatureCommand() { DeltaY = signY };
            return new CreatureCommand();
        }

        private bool CanGo(int x, int y)
            => !Tool.IsACharacter(x, y, Character.Terrain)
            && !Tool.IsACharacter(x, y, Character.Sack)
            && !Tool.IsACharacter(x, y, Character.Monster);

        public bool DeadInConflict(ICreature conflictedObject)
            => conflictedObject.GetDrawingPriority() == (int)Character.Sack
            || conflictedObject.GetDrawingPriority() == (int)Character.Monster;

        public int GetDrawingPriority()
            => (int)Character.Monster;

        public string GetImageFileName()
            => "Monster.png";

        private int[] GetPlayerCoord()
        {
            for (int i = 0; i < Game.MapWidth; ++i)
                for (int j = 0; j < Game.MapHeight; ++j)
                    if (Tool.IsACharacter(i, j, Character.Player))
                        return new int[] { i, j };
            return null;
        }
    }

    enum Character
    {
        Terrain, Player, Sack, Gold, Monster
    }

    static class Tool
    {
        public static bool InMap(int x, int y)
            => 0 <= x && x < Game.MapWidth && 0 <= y && y < Game.MapHeight;

        public static bool IsNothing(int x, int y)
            => Game.Map[x, y] == null;

        public static bool IsACharacter(int x, int y, Character creature)
            => !IsNothing(x, y) && Game.Map[x, y].GetDrawingPriority() == (int)creature;

        public static bool IsMonsterOrPlayer(int x, int y)
            => Tool.IsACharacter(x, y, Character.Player)
            || Tool.IsACharacter(x, y, Character.Monster);
    }
}

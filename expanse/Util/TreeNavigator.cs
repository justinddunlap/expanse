using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Util
{
    public class TreeNavigator<TNode, TLeaf> where TNode : class, TLeaf where TLeaf : class
    {
        Func<TLeaf, TNode> getParent;
        Func<TNode, IList<TLeaf>> getChildren;

        public TreeNavigator(Func<TLeaf, TNode> getParent, Func<TNode, IList<TLeaf>> getChildren)
        {
            this.getParent = getParent;
            this.getChildren = getChildren;
        }

        public TLeaf[] GetAncestors(TLeaf start, TNode stopAt = null, bool includeStart = false)
        {
            if (start == null)
                return null;
            List<TLeaf> ancestors = new List<TLeaf>();
            if (includeStart)
                ancestors.Add(start);
            TNode current = getParent(start);
            while (true)
            {
                if (current == null)
                    break;
                ancestors.Add(current);
                if (object.ReferenceEquals(current, stopAt))
                    break;
                current = getParent(current);
            }
            ancestors.Reverse();
            return ancestors.ToArray();
        }

        public TLeaf[] GetAncestors(TLeaf start, TNode stopAt = null, bool includeStart = false, Func<TLeaf, bool> condition = null)
        {
            if (start == null)
                return null;
            List<TLeaf> ancestors = new List<TLeaf>();
            if (includeStart)
            {
                if (condition(start))
                    ancestors.Add(start);
                else
                    return null;
            }
            TNode current = getParent(start);
            while (true)
            {
                if (current == null)
                    break;
                if (condition != null && condition((TLeaf)current))
                    ancestors.Add(current);
                else
                    return null;
                if (object.ReferenceEquals(current, stopAt))
                    break;
                current = getParent(current);
            }
            ancestors.Reverse();
            return ancestors.ToArray();
        }

        public bool IsDescendentOf(TLeaf node, TNode possibleAncestor)
        {
            TNode current = getParent(node);
            while (current != null)
            {
                if (object.ReferenceEquals(possibleAncestor, current))
                    return true;
                current = getParent(current);
            }
            return false;
        }

        public TLeaf GetDeepestCommonAncestor(IList<TLeaf> path1, IList<TLeaf> path2)
        {
            int shallowestPathDepth = Math.Min(path1.Count, path2.Count);
            TLeaf ret = default(TLeaf);
            for (int i = 0; i < shallowestPathDepth; i++)
            {
                if (object.ReferenceEquals(path1[i], path2[i]))
                    ret = path1[i];
                else
                    break;
            }
            return ret;
        }

        public TNode GetDeepestCommonAncestor(IList<TNode> path1, IList<TNode> path2)
        {
            int shallowestPathDepth = Math.Min(path1.Count, path2.Count);
            TNode ret = default(TNode);
            for (int i = 0; i < shallowestPathDepth; i++)
            {
                if (object.ReferenceEquals(path1[i], path2[i]))
                    ret = path1[i];
                else
                    break;
            }
            return ret;
        }

        public int GetDeepestCommonAncestorIndex(IList<TNode> path1, IList<TNode> path2)
        {
            int shallowestPathDepth = Math.Min(path1.Count, path2.Count);
            int idx = -1;
            for (int i = 0; i < shallowestPathDepth; i++)
            {
                if (object.ReferenceEquals(path1[i], path2[i]))
                    idx = i;
                else
                    break;
            }
            return idx;
        }

        public int GetDeepestCommonAncestorIndex(IList<TLeaf> path1, IList<TLeaf> path2)
        {
            int shallowestPathDepth = Math.Min(path1.Count, path2.Count);
            int idx = -1;
            for (int i = 0; i < shallowestPathDepth; i++)
            {
                if (object.ReferenceEquals(path1[i], path2[i]))
                    idx = i;
                else
                    break;
            }
            return idx;
        }

        public TreePathIntersection<TLeaf> GetPathIntersection(TLeaf elem1, TLeaf elem2, Func<TLeaf, bool> path1Condition, Func<TLeaf, bool> path2Condition)
        {
            TLeaf[] path1 = GetAncestors(elem1, default(TNode), true);
            TLeaf[] path2 = GetAncestors(elem2, default(TNode), true);
            int commonAncestorIndex = (path1 == null || path2 == null ? -1 : GetDeepestCommonAncestorIndex(path1, path2));
            bool firstPathConditionMet = true;
            bool secondPathConditionMet = true;
            if (path1 != null && path1Condition != null)
            {
                ReverseWhile(
                        path1,
                        (s, i) => !(firstPathConditionMet = path1Condition(s)),
                        0, commonAncestorIndex
                 );
            }

            if (path2 != null && path2Condition != null)
            {
                While(
                        path2,
                        (s, i) => !(secondPathConditionMet = path2Condition(s)),
                        commonAncestorIndex + 1
                    );
            }

            return new TreePathIntersection<TLeaf>(path1, path2, commonAncestorIndex, firstPathConditionMet, secondPathConditionMet);
        }

        private void ReverseWhile<T>(IList<T> list, Func<T, int, bool> action, int startOffset, int endIndex)
        {
            for (int i = list.Count - (1 + startOffset); i > endIndex; i--)
                if (!action(list[i], i))
                    break;

        }

        private static void While<T>(IEnumerable<T> enumerable,  Func<T, int, bool> action, int start)
        {
            int i = start;
            foreach (T t in enumerable)
            {
                if (action(t, i))
                    break;
                i++;
            }
        }

        public TreePathIntersection<TLeaf> GetPathIntersection(TLeaf elem1, TLeaf elem2)
        {
            TLeaf[] path1 = GetAncestors(elem1, default(TNode), true);
            TLeaf[] path2 = GetAncestors(elem2, default(TNode), true);
            return new TreePathIntersection<TLeaf>(path1, path2, GetDeepestCommonAncestorIndex(path1, path2));
        }
    }

    public class TreePathIntersection<T>
    {
        public TreePathIntersection(IList<T> path1, IList<T> path2, int commonAncestorIndex)
        {
            this.path1 = path1; this.path2 = path2; this.commonAncestorIndex = commonAncestorIndex;
            this.firstPathMetCondition = true;
            this.secondPathMetCondition = true;
        }

        public TreePathIntersection(IList<T> path1, IList<T> path2, int commonAncestorIndex, bool firstPathMetCondition, bool secondPathMetCondition)
        {
            this.path1 = path1; this.path2 = path2; this.commonAncestorIndex = commonAncestorIndex;
            this.firstPathMetCondition = firstPathMetCondition;
            this.secondPathMetCondition = secondPathMetCondition;
        }

        private int commonAncestorIndex;

        public int CommonAncestorIndex
        {
            get { return commonAncestorIndex; }
        }

        private IList<T> path1;

        public IList<T> Path1
        {
            get { return path1; }
        }

        private IList<T> path2;

        public IList<T> Path2
        {
            get { return path2; }
        }

        private bool firstPathMetCondition;

        public bool FirstPathMetCondition
        {
            get { return firstPathMetCondition; }
        }

        private bool secondPathMetCondition;

        public bool SecondPathMetCondition
        {
            get { return secondPathMetCondition; }
        }

    }

}

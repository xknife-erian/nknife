package net.xknife.lang.interfaces;

import java.util.List;

public interface ITreeNode
{
	boolean isRoot();

	ITreeNode getParent();

	List<ITreeNode> getChildren();
}

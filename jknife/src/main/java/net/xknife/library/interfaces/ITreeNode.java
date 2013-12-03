package net.xknife.library.interfaces;

import java.util.List;

public interface ITreeNode
{
	boolean isRoot();

	ITreeNode getParent();

	List<ITreeNode> getChildren();
}

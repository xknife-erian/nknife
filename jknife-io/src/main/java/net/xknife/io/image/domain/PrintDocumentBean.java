package net.xknife.io.image.domain;

/**
 * 打印服务对象定义
 *
 * Created by yangjuntao@xknife.net on 2014/7/15 0015.
 */
public class PrintDocumentBean
{
    private String id;
    private String top;
    private String left;
    private String width;
    private String height;
    private String content;

    public String getId()
    {
        return id;
    }

    public void setId(String id)
    {
        this.id = id;
    }

    public String getTop()
    {
        return top;
    }

    public void setTop(String top)
    {
        this.top = top;
    }

    public String getLeft()
    {
        return left;
    }

    public void setLeft(String left)
    {
        this.left = left;
    }

    public String getWidth()
    {
        return width;
    }

    public void setWidth(String width)
    {
        this.width = width;
    }

    public String getHeight()
    {
        return height;
    }

    public void setHeight(String height)
    {
        this.height = height;
    }

    public String getContent()
    {
        return content;
    }

    public void setContent(String content)
    {
        this.content = content;
    }

    @Override
    public String toString()
    {
        return "PrintDocumentBean{" +
                "id='" + id + '\'' +
                ", top='" + top + '\'' +
                ", left='" + left + '\'' +
                ", width='" + width + '\'' +
                ", height='" + height + '\'' +
                ", content='" + content + '\'' +
                '}';
    }
}

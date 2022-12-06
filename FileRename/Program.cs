Console.WriteLine("使用说明：将指定目录下包含aaa的文件，替换为bbb的文件");
Console.Write("使用格式：");
Console.WriteLine("rename.exe 'd:\\test' 'aaa' 'bbb'");
Console.WriteLine("");
Console.WriteLine("");
string? path = "";
string? preview_key ="";
string? next_key ="";

if (args.Length!=3)
{
    Console.WriteLine("当前【命令行输入】模式");
    file(ref path, ref preview_key, ref next_key);  
}
else
{
    path = args[0];
    preview_key = args[1];
    next_key = args[2];
}


List<string> filelist = new List<string>();
FullFileList(new DirectoryInfo(path));

Console.WriteLine($"输出列表：");
foreach (var item in filelist)
{
    if(!string.IsNullOrEmpty(preview_key))
    {
        File.Move(item, item.Replace(preview_key, next_key));
    }
    string filename = item.Substring(item.LastIndexOf("\\")+1);
    string targetname = filename.Replace(preview_key, next_key);
    if (filename!=targetname)
    {
        Console.WriteLine("");
        Console.WriteLine($"原始文件名：{filename}");
        Console.WriteLine($"目标文件名：{filename.Replace(preview_key, next_key)}");
    }
}

void file(ref string? path,ref string? preview_key, ref string? next_key)
{
    Console.Write("请输入目标目录：");
    path = Console.ReadLine();
    if (!Directory.Exists(path))
    {
        Console.WriteLine("不存在的目径,请重新操作");
        file(ref path, ref preview_key, ref next_key);
    }
    Console.Write("请输入替换前的关键字：");
    preview_key = Console.ReadLine();
    Console.Write("请输入替换后的字：");
    next_key = Console.ReadLine();
}


void FullFileList(DirectoryInfo folder)
{
    FileInfo[] files = folder.GetFiles();
    FileInfo file;

    for (int i = files.GetLowerBound(0); i <= files.GetUpperBound(0); i++)
    {
        file = files[i];
        if (file.Name.CompareTo("Thumbs.db") != 0)
        {
            filelist.Add(file.FullName);
        }
    }
    DirectoryInfo[] folders = folder.GetDirectories();
    DirectoryInfo f;
    for (int i = folders.GetLowerBound(0); i <= folders.GetUpperBound(0); i++)
    {
        f = folders[i];
        FullFileList(f);
    }
}
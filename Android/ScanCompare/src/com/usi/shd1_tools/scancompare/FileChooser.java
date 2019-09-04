package com.usi.shd1_tools.scancompare;

import java.io.File;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;
import android.os.Bundle;
import android.view.View;
import android.app.ListActivity;
import android.content.Intent;
import android.widget.ListView;
import android.widget.Toast;

public class FileChooser extends ListActivity {

	public static final String ACTION_FileChosen = "FileChooser.Action.Chosen";
	public static final String EXTRA_ChosenFileFullPath = "FileChooser.Extra.ChosenFileFullPath";
	public static final String EXTRA_ChosenFileName = "FileChooser.Extra.ChosenFileName";
	
	private int continuallyBackkeyClickCount=0;	
    private File currentDir;
    private FileArrayAdapter adapter;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//setContentView(R.layout.activity_filechooser);
        currentDir = new File("/sdcard/");
        fill(currentDir);
	}
	
	private void fill(File f)
    {
    	File[]dirs = f.listFiles();
		 this.setTitle("Current Dir: "+f.getName());
		 List<Option>dir = new ArrayList<Option>();
		 List<Option>fls = new ArrayList<Option>();
		 try{
			 for(File ff: dirs)
			 {
				if(ff.isDirectory())
				{	
					String dirName = ff.getAbsolutePath();
					dir.add(new Option(ff.getName(),"Folder",dirName));
				}
				else
				{
					String fileName = ff.getAbsolutePath();
					fls.add(new Option(ff.getName(),"File Size: "+ff.length(),fileName));
				}
			 }
		 }catch(Exception e)
		 {
			 
		 }
		 Collections.sort(dir);
		 Collections.sort(fls);
		 dir.addAll(fls);
		 if(!f.getName().equalsIgnoreCase("sdcard"))
			 dir.add(0,new Option("..","Parent Directory",f.getParent()));
		 adapter = new FileArrayAdapter(FileChooser.this,R.layout.file_view,dir);
		 this.setListAdapter(adapter);
    }
	
    @Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);
		Option o = adapter.getItem(position);
		if(o.getData().equalsIgnoreCase("folder")||o.getData().equalsIgnoreCase("parent directory")){
				currentDir = new File(o.getPath());
				fill(currentDir);
		}
		else
		{
			onFileClick(o);
		}
		continuallyBackkeyClickCount = 0;
	}
    
    private void onFileClick(Option o)
    {
    	Toast.makeText(this, "File selected : "+o.getName(), Toast.LENGTH_SHORT).show();
    	Intent intent = new Intent();
    	intent.setAction(ACTION_FileChosen);
    	intent.putExtra(EXTRA_ChosenFileFullPath, o.getPath());
    	intent.putExtra(EXTRA_ChosenFileName, o.getName());
    	sendBroadcast(intent);
    	super.onBackPressed();
    }
    
    Toast backNoticeToast;
    private Timer continuallyBackkeyClickTimer;
    @Override
    public void onBackPressed(){
    	if(continuallyBackkeyClickTimer!=null){
    		continuallyBackkeyClickTimer.cancel();
    	}
    	continuallyBackkeyClickTimer = new Timer();
		continuallyBackkeyClickTimer.schedule(new TimerTask() {
			@Override
			public void run() {
				continuallyBackkeyClickCount = 0;
				continuallyBackkeyClickTimer.cancel();
			}
		},3000);
		continuallyBackkeyClickCount++;		
		if(continuallyBackkeyClickCount==1){
			backNoticeToast = Toast.makeText(this,"Click back key again to exit file chooser.",Toast.LENGTH_SHORT);
			backNoticeToast.show();
		}
		else if(continuallyBackkeyClickCount>=2){
			continuallyBackkeyClickCount = 0;
			continuallyBackkeyClickTimer.cancel();
			if(backNoticeToast!=null){
				backNoticeToast.cancel();
			}
			super.onBackPressed();
		}    	
    }   
}


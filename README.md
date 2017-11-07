# topography-example-wff

A small project with a franken-topography of windows services technologies and endpoints, just to show it all working together.

## dotnet core web application with
1. rest api
2. web front end in mvc
3. bootstrap

## Publishing:
1. Set up two Web sites in IIS, examples: Sample and SampleAPI.
2. For ease of debugging, you can give each website it's own new threadpool, or use the DefaultAppPool.
3. Select different ports for each site.

## Windows Directory security
3. In the target Windows directories where the physical files live, you will need to give the IIS threadpool read access.
4. Go to the Windows directory select properties -> security for the given directory. Click "Edit" then "Add"
5. You should be on Location=the-local-machine. If not, click "Locations" and choose that, (not the Domain).
6. type "IIS AppPool\DefaultAppPool" <- replace "DefaultAppPool" with your new apppool name if in step Publishing-step-#2 you decided to use a different app pool.
7. Click "Check Names" and then "OK".

## cc-post-processor
the post-processor is a plugin for cloud compute that reads dss files as a post process and pulls the peak of the dss file. 

## contributing
pull the code
open in visual studio code
add remote development extension
reopen vs code in container defined by docker.dev
set up nuget.config
    <?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="hec-dss-nuget" value="https://www.hec.usace.army.mil/nexus/repository/dss/" />
    <add key="usace-cc-sdk" value="https://nuget.pkg.github.com/USACE/index.json" />
  </packageSources>
    <packageSourceCredentials>
        <usace-cc-sdk>
            <add key="Username" value="yourgithubname" />
            <add key="ClearTextPassword" value="personal token with package read permission" />
        </usace-cc-sdk>
    </packageSourceCredentials>
</configuration>
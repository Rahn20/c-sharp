#!/usr/bin/bash
#
# Exit values:
#  0 on success
#  1 on failure


function main() {

    case "$1" in
        db)
            echo "Run migrations and update database."

            # Add-Migration InitialCreate
            # Update-Databas
            dotnet ef migrations add InitialCreate
            dotnet ef database update
            exit 0
        ;;
        clean)
            echo "Cleaning /bin and /obj folders..."
            
            # List of directories to clean
            local dirsToClean=(
                "UtilitiesLib/bin"
                "UtilitiesLib/obj"
                "MediaPlaylistStore/bin"
                "MediaPlaylistStore/obj"
                "MediaPlaylistDAL/bin"
                "MediaPlaylistDAL/obj"
                "MediaPlaylistBL/bin"
                "MediaPlaylistBL/obj"
                "MediaPlaylist/bin"
                "MediaPlaylist/obj"
            )

            # Loop through each directory and remove it if it exists
            for dir in "${dirsToClean[@]}"; do

                if [ -d "$dir" ]; then
                    rm -rf "$dir"
                    echo "Removed $dir"
                else 
                    echo "Directory not found, skipping $dir"
                fi

            done
        ;;
        *)
            printf "Invalid option.\\n"
            exit 1
        ;;
    esac
}

main "$@"

# bash project.sh clean
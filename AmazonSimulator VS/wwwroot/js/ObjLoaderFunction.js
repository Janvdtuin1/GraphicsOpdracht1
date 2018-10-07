/**
 * 
 * @param {string} modelPath Het pad naar het model op de server
 * @param {string} modelName De naam van het model in de map (OBJ bestand)
 * @param {string} texturePath Het pad naar de texture van het model
 * @param {string} textureName De naam van het texturebestand (MTL)
 * @param {function(THREE.Mesh): void} onload Die functie die aangeroepen wordt als het model geladen is
 * @return {void}

 */

function LoadOBJModel(modelPath, modelName, texturePath, textureName, onload) {
    new THREE.MTLLoader()
        .setPath(texturePath)
        .load(textureName, function (materials) {
            materials.preload();

            new THREE.OBJLoader()
                .setPath(modelPath)
                .setMaterials(materials)
                .load(modelName, function (object) {
                    onload(object);


                }, function () { }, function (e) { console.log("Er is iets mis gegaan tijdens het laden van het model"); console.log(e); });

        });

        }


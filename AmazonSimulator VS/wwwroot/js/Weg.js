function WegEnStoep() {
    var weggroup = new THREE.Group();
    //weg met stoep-
    var weggeometry = new THREE.PlaneGeometry(100, 10, 32);

    var wegmateriaal = new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/weg/weg.jpg"), side: THREE.DoubleSide });
    var wegplane = new THREE.Mesh(weggeometry, wegmateriaal);
    wegplane.rotation.x = Math.PI / 2.0;
    wegplane.position.x = 15;
    wegplane.position.z = 35;

    weggroup.add(wegplane);
    var stoepgeometry = new THREE.PlaneGeometry(100, 5, 32);
    var stoeptexture = new THREE.TextureLoader().load("textures/weg/stonebrick.png");
    stoeptexture.wrapS = THREE.RepeatWrapping;
    stoeptexture.wrapT = THREE.RepeatWrapping;
    stoeptexture.repeat.set(16,2);


    var stoepmateriaal = new THREE.MeshStandardMaterial({ map: stoeptexture, side: THREE.DoubleSide});

    var stoepplane = new THREE.Mesh(stoepgeometry, stoepmateriaal);
    stoepplane.rotation.x = Math.PI / 2.0;
    stoepplane.position.x = 15;
    stoepplane.position.z = 42.5;
    weggroup.add(stoepplane);
    return weggroup;
    
}
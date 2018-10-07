function TruckModel() {
    var TruckGroup = new THREE.Group();

    LoadOBJModel("obj/Truck/", "truck.obj", "obj/Truck/", "truck.mtl", (mesh) => {
        mesh.rotation.y = 9.40;
        mesh.scale.set(0.025, 0.025, 0.025);
        TruckGroup.add(mesh);


    })

    return TruckGroup;



}
function StraatLantaarns() {
    var LantaarnGroup = new THREE.Group();

    LoadOBJModel("obj/StraatLantaarns/", "lamp.obj", "obj/StraatLantaarns/", "lamp.mtl", (mesh) => {
        mesh.position.z = 42;
        mesh.scale.set(0.5, 0.5, 0.5);
        LantaarnGroup.add(mesh);


    })

    return LantaarnGroup;



}